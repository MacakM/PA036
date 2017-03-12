<?php
namespace AppBundle\Controller;

use AppBundle\Entity\Customer;
use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
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
        $stopwatch = new Stopwatch();
        $stopwatch->start('testEvent');
        $entities = $this->getDoctrine()->getManager()->getRepository(Customer::class)->findAll();
        $event = $stopwatch->stop('testEvent');
        return $this->render('base/index.html.twig', [
            'entities' => $entities,
            'event' => $event
        ]);
    }

    /**
     * @Route("/base/{experimentId}", name="base_api_experiment")
     * @param Request $request
     * @param $experimentId
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function experimentAction(Request $request,$experimentId)
    {
        // replace this example code with whatever you need
        return $this->render('default/index.html.twig', [
            'base_dir' => realpath($this->getParameter('kernel.root_dir').'/..').DIRECTORY_SEPARATOR,
        ]);
    }
}