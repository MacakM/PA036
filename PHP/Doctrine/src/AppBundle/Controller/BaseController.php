<?php
namespace AppBundle\Controller;

use AppBundle\Entity\Customer;
use AppBundle\Entity\Nation;
use AppBundle\Entity\Orders;
use Doctrine\ORM\EntityManager;
use Doctrine\ORM\Tools\Pagination\Paginator;
use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\Stopwatch\Stopwatch;

class BaseController extends Controller
{

    /**
     * @Route("/base", name="base_api")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function indexAction(Request $request)
    {
        die();
        $stopwatch = new Stopwatch();
        $stopwatch->start('testEvent');
        $entities = $this->getDoctrine()->getManager()->getRepository(Customer::class)->findFirstNCustomers(3000);
        $entities2 = $this->getDoctrine()->getManager()->getRepository(Customer::class)->findFirstNCustomersWithCache(3000);
        $entities3 = $this->getDoctrine()->getManager()->getRepository(Customer::class)->findFirstNCustomersWithCache(3000);
        $entities = $this->getDoctrine()->getManager()->getRepository(Orders::class)->findBy(["oOrderstatus" => "P"]);
        $event = $stopwatch->stop('testEvent');
        return $this->render('base/index.html.twig', [
            'entities' => $entities,
            'event' => $event
        ]);
    }

    /**
     * Should produce 1 db query
     * @Route("/base/1", name="base_api_experiment_1")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment1Action(Request $request)
    {
        for ($i = 0; $i < 3; $i++) {
            $customer = $this->getEm()->getRepository(Customer::class)->find(1);
        }
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * Should produce 1 db query
     * @Route("/base/2", name="base_api_experiment_2")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment2Action(Request $request)
    {
        for ($i = 0; $i < 3; $i++) {
            //useResultCache(true) enables internal cache of result for given query
            $customers = $this->getEm()->createQuery('SELECT o FROM '.Orders::class.' o WHERE o.oTotalprice < 1000')->useResultCache(true)->getResult();
        }
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * Should produce 3 db queries
     * @Route("/base/3", name="base_api_experiment_3")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment3Action(Request $request)
    {
        $customers = $this->getEm()->createQuery('SELECT c FROM '.Customer::class.' c WHERE c.cCustkey < 1000')->useResultCache(true)->getResult();
        $customers = $this->getEm()->createQuery('SELECT c FROM '.Customer::class.' c WHERE c.cCustkey < 250')->useResultCache(true)->getResult();
        $customers = $this->getEm()->createQuery('SELECT c FROM '.Customer::class.' c WHERE c.cCustkey < 10')->useResultCache(true)->getResult();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * Should produce 2 db queries before cached
     * @Route("/base/4", name="base_api_experiment_4")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment4Action(Request $request)
    {
        $orderswithCustomers = $this->getEm()->createQuery('SELECT o,c FROM '.Orders::class.' o JOIN o.oCustkey c where c.cAcctbal > 10 and c.cAcctbal < 100')->setCacheable(true)->getResult();
        $orderswithCustomers = $this->getEm()->createQuery('SELECT c,n FROM '.Customer::class.' c JOIN c.cNationkey n where c.cAcctbal > 10 and c.cAcctbal < 100')->getResult();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * Should produce 2 db queries before cached
     * @Route("/base/5", name="base_api_experiment_5")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment5Action(Request $request)
    {
        $query1 = $this->getEm()->createQuery('SELECT c FROM ' . Customer::class . ' c ORDER BY c.cCustkey')
            ->setFirstResult(0)->setMaxResults(100)->getResult();
        $query2 = $this->getEm()->createQuery('SELECT c FROM ' . Customer::class . ' c ORDER BY c.cCustkey')
            ->setFirstResult(0)->setMaxResults(150)->getResult();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * Should produce 3 db queries
     * @Route("/base/6", name="base_api_experiment_6")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment6Action(Request $request)
    {
        $customers = $this->getEm()->createQuery('SELECT c FROM '.Customer::class.' c WHERE c.cCustkey < 500')->useResultCache(true)->getResult();
        $customers = $this->getEm()->createQuery('SELECT c FROM '.Customer::class.' c WHERE c.cCustkey < 501')->useResultCache(true)->getResult();
        $customers = $this->getEm()->createQuery('SELECT c FROM '.Customer::class.' c WHERE c.cCustkey < 520')->useResultCache(true)->getResult();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }


    /**
     * @Route("/base/9", name="base_api_experiment_9")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment9Action(Request $request)
    {
        $customer = $this->getEm()->getRepository(Customer::class)->find(1);
        $customer->setCComment((new \DateTime())->getTimestamp());
        $this->getEm()->flush($customer);
        $customer2 = $this->getEm()->getRepository(Customer::class)->find(1);
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @Route("/base/10", name="base_api_experiment_10")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment10Action(Request $request)
    {
        $orders = $this->getEm()->createQuery('SELECT o FROM '.Orders::class.' o WHERE o.oTotalprice < 1000')->setCacheable(true)->getResult();
        $orders = $this->getEm()->createQuery('SELECT o FROM '.Orders::class.' o WHERE o.oTotalprice < 1000')->setCacheable(true)->getResult();
        /** @var Orders $order */
        $order = reset($orders);
        $order->setOComment((new \DateTime())->getTimestamp());
        $this->getEm()->flush($order);
        $orders = $this->getEm()->createQuery('SELECT o FROM '.Orders::class.' o WHERE o.oTotalprice < 1000')->setCacheable(true)->getResult();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @Route("/base/11", name="base_api_experiment_11")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment11Action(Request $request)
    {
        $customer = $this->createCust();
        $this->getEm()->persist($customer);
        $this->getEm()->flush($customer);
        $custId = $customer->getCCustkey();
        $customer = $this->getEm()->getRepository(Customer::class)->find($custId);
        $this->getEm()->remove($customer);
        $this->getEm()->flush($customer);
        $customer = $this->getEm()->getRepository(Customer::class)->find($custId);
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @Route("/base/12", name="base_api_experiment_12")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment12Action(Request $request)
    {
        $order = $this->createOrder();
        $this->getEm()->persist($order);
        $this->getEm()->flush();
        $orders = $this->getEm()->createQuery('SELECT o FROM '.Orders::class.' o where o.oTotalprice < 1000')->setCacheable(true)->getResult();
        $this->getEm()->remove($order);
        $this->getEm()->flush();
        $orders = $this->getEm()->createQuery('SELECT o FROM '.Orders::class.' o where o.oTotalprice < 1000')->setCacheable(true)->getResult();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @Route("/base/13", name="base_api_experiment_13")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment13Action(Request $request)
    {
        $customer = $this->createCust();
        $this->getEm()->persist($customer);
        $this->getEm()->flush($customer);
        $customer2 = $this->getEm()->getRepository(Customer::class)->find($customer->getCCustkey());
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @Route("/base/14", name="base_api_experiment_14")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment14Action(Request $request)
    {
        $orders = $this->getEm()->createQuery('SELECT o FROM '.Orders::class.' o where o.oTotalprice < 1000')->setCacheable(true)->getResult();
        $order = $this->createOrder();
        $this->getEm()->persist($order);
        $this->getEm()->flush();
        $orders = $this->getEm()->createQuery('SELECT o FROM '.Orders::class.' o where o.oTotalprice < 1000')->setCacheable(true)->getResult();
        $this->getEm()->remove($order);
        $this->getEm()->flush();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @Route("/base/16", name="base_api_experiment_16")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment16Action(Request $request)
    {
        $runTime = (new \DateTime())->getTimestamp();
        /** @var Customer[] $customers */
        $customers = $this->getEm()->createQuery('SELECT c FROM ' . Customer::class . ' c where c.cCustkey < 750')->setCacheable(true)->getResult();
        $customers = $this->getEm()->createQuery('SELECT c FROM ' . Customer::class . ' c where c.cCustkey < 750')->setCacheable(true)->getResult();

        $id = reset($customers)->getCCustkey();
        $this->getEm()->getConnection()->executeQuery('UPDATE CUSTOMER SET C_NAME = :time WHERE C_CUSTKEY = :id', ['time' => $runTime, 'id' => $id]);
        $customers = $this->getEm()->createQuery('SELECT c FROM ' . Customer::class . ' c where c.cCustkey < 750')->setCacheable(true)->getResult();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @Route("/base/17", name="base_api_experiment_17")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment17Action(Request $request)
    {
        /** @var Customer[] $customers */
        $customers = $this->getEm()->createQuery('SELECT c,o FROM ' . Customer::class . ' c join c.orders o where c.cAcctbal > 10 and c.cAcctbal < 100')->setCacheable(true)->getResult();
        foreach ($customers as $customer) {
            $customer->getOrders();
        }
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @Route("/base/18", name="base_api_experiment_18")
     * @param Request $request
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experiment18Action(Request $request)
    {
        /** @var Customer[] $customers */
        $customers = $this->getEm()->createQuery('SELECT c FROM ' . Customer::class . ' c where c.cAcctbal > 10 and c.cAcctbal < 100')->getResult();
        foreach($customers as $customer) {
            $customer->getOrders();
        }
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    private function createCust(){
        $customer = new Customer();
        $customer->setCNationkey($this->getEm()->getRepository(Nation::class)->find(1));
        $customer->setCName("Cust");
        $customer->setCAddress('B');
        $customer->setCComment('best customer');
        $customer->setCAcctbal(0);
        $customer->setCPhone('1');
        $customer->setCMktsegment("lolek");
        return $customer;
    }

    private function createOrder()
    {
        $order = new Orders();
        $order->setOCustkey($this->getEm()->getRepository(Customer::class)->find(1));
        $order->setOComment("furiously special f|");
        $order->setOOrderpriority("3-MEDIUM");
        $order->setOTotalprice(851);
        $order->setOClerk("Peter");
        $order->setOOrderdate(new \DateTime());
        $order->setOOrderstatus("0");
        $order->setOShippriority(0);
        return $order;
    }

    /**
     * @return EntityManager
     */
    private function getEm(){
        return $this->getDoctrine()->getManager();
    }
}