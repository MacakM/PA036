<?php

namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;
use Doctrine\ORM\Mapping\Cache;

/**
 * Customer
 *
 * @ORM\Table(name="CUSTOMER", indexes={@ORM\Index(name="IDX_470A124522864F86", columns={"C_NATIONKEY"})})
 * @ORM\Entity(repositoryClass="AppBundle\Repository\CustomerRepository")
 * @Cache(usage="READ_ONLY",region="test_region")
 */
class Customer
{
    /**
     * @var string
     *
     * @ORM\Column(name="C_NAME", type="string", length=25, nullable=false)
     */
    private $cName;

    /**
     * @var string
     *
     * @ORM\Column(name="C_ADDRESS", type="string", length=40, nullable=false)
     */
    private $cAddress;

    /**
     * @var string
     *
     * @ORM\Column(name="C_PHONE", type="string", length=15, nullable=false)
     */
    private $cPhone;

    /**
     * @var string
     *
     * @ORM\Column(name="C_ACCTBAL", type="decimal", precision=15, scale=2, nullable=false)
     */
    private $cAcctbal;

    /**
     * @var string
     *
     * @ORM\Column(name="C_MKTSEGMENT", type="string", length=10, nullable=false)
     */
    private $cMktsegment;

    /**
     * @var string
     *
     * @ORM\Column(name="C_COMMENT", type="string", length=117, nullable=false)
     */
    private $cComment;

    /**
     * @var integer
     *
     * @ORM\Column(name="C_CUSTKEY", type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="IDENTITY")
     */
    private $cCustkey;

    /**
     * @var \AppBundle\Entity\Nation
     *
     * @ORM\ManyToOne(targetEntity="AppBundle\Entity\Nation")
     * @ORM\JoinColumns({
     *   @ORM\JoinColumn(name="C_NATIONKEY", referencedColumnName="N_NATIONKEY")
     * })
     */
    private $cNationkey;



    /**
     * Set cName
     *
     * @param string $cName
     *
     * @return Customer
     */
    public function setCName($cName)
    {
        $this->cName = $cName;
    
        return $this;
    }

    /**
     * Get cName
     *
     * @return string
     */
    public function getCName()
    {
        return $this->cName;
    }

    /**
     * Set cAddress
     *
     * @param string $cAddress
     *
     * @return Customer
     */
    public function setCAddress($cAddress)
    {
        $this->cAddress = $cAddress;
    
        return $this;
    }

    /**
     * Get cAddress
     *
     * @return string
     */
    public function getCAddress()
    {
        return $this->cAddress;
    }

    /**
     * Set cPhone
     *
     * @param string $cPhone
     *
     * @return Customer
     */
    public function setCPhone($cPhone)
    {
        $this->cPhone = $cPhone;
    
        return $this;
    }

    /**
     * Get cPhone
     *
     * @return string
     */
    public function getCPhone()
    {
        return $this->cPhone;
    }

    /**
     * Set cAcctbal
     *
     * @param string $cAcctbal
     *
     * @return Customer
     */
    public function setCAcctbal($cAcctbal)
    {
        $this->cAcctbal = $cAcctbal;
    
        return $this;
    }

    /**
     * Get cAcctbal
     *
     * @return string
     */
    public function getCAcctbal()
    {
        return $this->cAcctbal;
    }

    /**
     * Set cMktsegment
     *
     * @param string $cMktsegment
     *
     * @return Customer
     */
    public function setCMktsegment($cMktsegment)
    {
        $this->cMktsegment = $cMktsegment;
    
        return $this;
    }

    /**
     * Get cMktsegment
     *
     * @return string
     */
    public function getCMktsegment()
    {
        return $this->cMktsegment;
    }

    /**
     * Set cComment
     *
     * @param string $cComment
     *
     * @return Customer
     */
    public function setCComment($cComment)
    {
        $this->cComment = $cComment;
    
        return $this;
    }

    /**
     * Get cComment
     *
     * @return string
     */
    public function getCComment()
    {
        return $this->cComment;
    }

    /**
     * Get cCustkey
     *
     * @return integer
     */
    public function getCCustkey()
    {
        return $this->cCustkey;
    }

    /**
     * Set cNationkey
     *
     * @param \AppBundle\Entity\Nation $cNationkey
     *
     * @return Customer
     */
    public function setCNationkey(\AppBundle\Entity\Nation $cNationkey = null)
    {
        $this->cNationkey = $cNationkey;
    
        return $this;
    }

    /**
     * Get cNationkey
     *
     * @return \AppBundle\Entity\Nation
     */
    public function getCNationkey()
    {
        return $this->cNationkey;
    }
}
