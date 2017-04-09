<?php

 /**
 * CTI Digital
 *
 * @author Jason Brown <j.brown@ctidigital.com>
 */

namespace AppBundle\Model\DBAL\Driver\MSSql;

/**
 * Class LastInsertId
 * @package Application\Database\Doctrine\DBAL\Driver\MSSql
 */
class LastInsertId
{
    /**
     * @var integer
     */
    private $id;

    /**
     * @param integer $id
     */
    public function setId($id)
    {
        $this->id = $id;
    }

    /**
     * @return integer
     */
    public function getId()
    {
        return $this->id;
    }
}