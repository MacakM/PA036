<?php
namespace AppBundle\Repository;

use Doctrine\ORM\EntityRepository;
use Doctrine\ORM\QueryBuilder;

class CustomerRepository extends EntityRepository
{
    public function findFirstNCustomersWithCache($n){
        $qb = new QueryBuilder($this->getEntityManager());
        $qb->select('c')
            ->from('AppBundle:Customer','c')
            ->setMaxResults($n);
        return $qb->getQuery()->useResultCache(true)->getResult();
    }

    public function findFirstNCustomers($n){
        $qb = new QueryBuilder($this->getEntityManager());
        $qb->select('c')
            ->from('AppBundle:Customer','c')
            ->setMaxResults($n);
        return $qb->getQuery()->getResult();
    }
}