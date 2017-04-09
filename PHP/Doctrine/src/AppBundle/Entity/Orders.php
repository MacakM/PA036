<?php

namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;
use Doctrine\ORM\Mapping\Cache;

/**
 * Orders
 *
 * @ORM\Table(name="ORDERS", indexes={@ORM\Index(name="IDX_15D73A689A2ED26D", columns={"O_CUSTKEY"})})
 * @ORM\Entity
 * @Cache(usage="READ_ONLY",region="test_region")
 */
class Orders
{
    /**
     * @var string
     *
     * @ORM\Column(name="O_ORDERSTATUS", type="string", length=1, nullable=false)
     */
    private $oOrderstatus;

    /**
     * @var string
     *
     * @ORM\Column(name="O_TOTALPRICE", type="decimal", precision=15, scale=2, nullable=false)
     */
    private $oTotalprice;

    /**
     * @var \DateTime
     *
     * @ORM\Column(name="O_ORDERDATE", type="date", nullable=false)
     */
    private $oOrderdate;

    /**
     * @var string
     *
     * @ORM\Column(name="O_ORDERPRIORITY", type="string", length=15, nullable=false)
     */
    private $oOrderpriority;

    /**
     * @var string
     *
     * @ORM\Column(name="O_CLERK", type="string", length=15, nullable=false)
     */
    private $oClerk;

    /**
     * @var integer
     *
     * @ORM\Column(name="O_SHIPPRIORITY", type="integer", nullable=false)
     */
    private $oShippriority;

    /**
     * @var string
     *
     * @ORM\Column(name="O_COMMENT", type="string", length=79, nullable=false)
     */
    private $oComment;

    /**
     * @var integer
     *
     * @ORM\Column(name="O_ORDERKEY", type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="IDENTITY")
     */
    private $oOrderkey;

    /**
     * @var \AppBundle\Entity\Customer
     *
     * @ORM\ManyToOne(targetEntity="AppBundle\Entity\Customer")
     * @ORM\JoinColumns({
     *   @ORM\JoinColumn(name="O_CUSTKEY", referencedColumnName="C_CUSTKEY")
     * })
     */
    private $oCustkey;



    /**
     * Set oOrderstatus
     *
     * @param string $oOrderstatus
     *
     * @return Orders
     */
    public function setOOrderstatus($oOrderstatus)
    {
        $this->oOrderstatus = $oOrderstatus;
    
        return $this;
    }

    /**
     * Get oOrderstatus
     *
     * @return string
     */
    public function getOOrderstatus()
    {
        return $this->oOrderstatus;
    }

    /**
     * Set oTotalprice
     *
     * @param string $oTotalprice
     *
     * @return Orders
     */
    public function setOTotalprice($oTotalprice)
    {
        $this->oTotalprice = $oTotalprice;
    
        return $this;
    }

    /**
     * Get oTotalprice
     *
     * @return string
     */
    public function getOTotalprice()
    {
        return $this->oTotalprice;
    }

    /**
     * Set oOrderdate
     *
     * @param \DateTime $oOrderdate
     *
     * @return Orders
     */
    public function setOOrderdate($oOrderdate)
    {
        $this->oOrderdate = $oOrderdate;
    
        return $this;
    }

    /**
     * Get oOrderdate
     *
     * @return \DateTime
     */
    public function getOOrderdate()
    {
        return $this->oOrderdate;
    }

    /**
     * Set oOrderpriority
     *
     * @param string $oOrderpriority
     *
     * @return Orders
     */
    public function setOOrderpriority($oOrderpriority)
    {
        $this->oOrderpriority = $oOrderpriority;
    
        return $this;
    }

    /**
     * Get oOrderpriority
     *
     * @return string
     */
    public function getOOrderpriority()
    {
        return $this->oOrderpriority;
    }

    /**
     * Set oClerk
     *
     * @param string $oClerk
     *
     * @return Orders
     */
    public function setOClerk($oClerk)
    {
        $this->oClerk = $oClerk;
    
        return $this;
    }

    /**
     * Get oClerk
     *
     * @return string
     */
    public function getOClerk()
    {
        return $this->oClerk;
    }

    /**
     * Set oShippriority
     *
     * @param integer $oShippriority
     *
     * @return Orders
     */
    public function setOShippriority($oShippriority)
    {
        $this->oShippriority = $oShippriority;
    
        return $this;
    }

    /**
     * Get oShippriority
     *
     * @return integer
     */
    public function getOShippriority()
    {
        return $this->oShippriority;
    }

    /**
     * Set oComment
     *
     * @param string $oComment
     *
     * @return Orders
     */
    public function setOComment($oComment)
    {
        $this->oComment = $oComment;
    
        return $this;
    }

    /**
     * Get oComment
     *
     * @return string
     */
    public function getOComment()
    {
        return $this->oComment;
    }

    /**
     * Get oOrderkey
     *
     * @return integer
     */
    public function getOOrderkey()
    {
        return $this->oOrderkey;
    }

    /**
     * Set oCustkey
     *
     * @param \AppBundle\Entity\Customer $oCustkey
     *
     * @return Orders
     */
    public function setOCustkey(\AppBundle\Entity\Customer $oCustkey = null)
    {
        $this->oCustkey = $oCustkey;
    
        return $this;
    }

    /**
     * Get oCustkey
     *
     * @return \AppBundle\Entity\Customer
     */
    public function getOCustkey()
    {
        return $this->oCustkey;
    }
}
