<?php
namespace AppBundle\Listener;

use Symfony\Component\DependencyInjection\Container;
use Symfony\Component\HttpKernel\Event\GetResponseEvent;

class RequestListener
{
    /**
     * @var Container
     */
    private $container;


    /**
     * RequestListener constructor.
     * @param Container $container
     */
    public function __construct(Container $container)
    {
        $this->container = $container;
    }

    public function onKernelRequest(GetResponseEvent $event)
    {
        if (!$event->isMasterRequest()) {
            // don't do anything if it's not the master request
            return;
        }
        //$this->container->get('doctrine')->getConnection()->getConfiguration()->setSQLLogger($this->container->get('pa036_sql_logger'));
    }
}