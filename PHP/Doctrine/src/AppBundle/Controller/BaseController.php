<?php
namespace AppBundle\Controller;

use AppBundle\Entity\Customer;
use AppBundle\Entity\Orders;
use Doctrine\ORM\EntityManager;
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
        $orderswithCustomers = $this->getEm()->createQuery('SELECT o,c FROM '.Orders::class.' o JOIN o.oCustkey c')->useResultCache(true)->getResult();
        $orderswithCustomers = $this->getEm()->createQuery('SELECT c,n FROM '.Customer::class.' c JOIN c.cNationkey n')->useResultCache(true)->getResult();
        return new JsonResponse($this->get('pa036_sql_logger')->queries);
    }

    /**
     * @return EntityManager
     */
    private function getEm(){
        return $this->getDoctrine()->getManager();
    }
}