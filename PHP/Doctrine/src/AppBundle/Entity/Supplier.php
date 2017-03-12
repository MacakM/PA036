<?php

namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
 * Supplier
 *
 * @ORM\Table(name="SUPPLIER", indexes={@ORM\Index(name="IDX_5D19F0328C708B4D", columns={"S_NATIONKEY"})})
 * @ORM\Entity
 */
class Supplier
{
    /**
     * @var string
     *
     * @ORM\Column(name="S_NAME", type="string", length=25, nullable=false)
     */
    private $sName;

    /**
     * @var string
     *
     * @ORM\Column(name="S_ADDRESS", type="string", length=40, nullable=false)
     */
    private $sAddress;

    /**
     * @var string
     *
     * @ORM\Column(name="S_PHONE", type="string", length=15, nullable=false)
     */
    private $sPhone;

    /**
     * @var string
     *
     * @ORM\Column(name="S_ACCTBAL", type="decimal", precision=15, scale=2, nullable=false)
     */
    private $sAcctbal;

    /**
     * @var string
     *
     * @ORM\Column(name="S_COMMENT", type="string", length=101, nullable=false)
     */
    private $sComment;

    /**
     * @var integer
     *
     * @ORM\Column(name="S_SUPPKEY", type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="IDENTITY")
     */
    private $sSuppkey;

    /**
     * @var \AppBundle\Entity\Nation
     *
     * @ORM\ManyToOne(targetEntity="AppBundle\Entity\Nation")
     * @ORM\JoinColumns({
     *   @ORM\JoinColumn(name="S_NATIONKEY", referencedColumnName="N_NATIONKEY")
     * })
     */
    private $sNationkey;

    /**
     * @var \Doctrine\Common\Collections\Collection
     *
     * @ORM\ManyToMany(targetEntity="AppBundle\Entity\Part", mappedBy="psSuppkey")
     */
    private $psPartkey;

    /**
     * Constructor
     */
    public function __construct()
    {
        $this->psPartkey = new \Doctrine\Common\Collections\ArrayCollection();
    }


    /**
     * Set sName
     *
     * @param string $sName
     *
     * @return Supplier
     */
    public function setSName($sName)
    {
        $this->sName = $sName;
    
        return $this;
    }

    /**
     * Get sName
     *
     * @return string
     */
    public function getSName()
    {
        return $this->sName;
    }

    /**
     * Set sAddress
     *
     * @param string $sAddress
     *
     * @return Supplier
     */
    public function setSAddress($sAddress)
    {
        $this->sAddress = $sAddress;
    
        return $this;
    }

    /**
     * Get sAddress
     *
     * @return string
     */
    public function getSAddress()
    {
        return $this->sAddress;
    }

    /**
     * Set sPhone
     *
     * @param string $sPhone
     *
     * @return Supplier
     */
    public function setSPhone($sPhone)
    {
        $this->sPhone = $sPhone;
    
        return $this;
    }

    /**
     * Get sPhone
     *
     * @return string
     */
    public function getSPhone()
    {
        return $this->sPhone;
    }

    /**
     * Set sAcctbal
     *
     * @param string $sAcctbal
     *
     * @return Supplier
     */
    public function setSAcctbal($sAcctbal)
    {
        $this->sAcctbal = $sAcctbal;
    
        return $this;
    }

    /**
     * Get sAcctbal
     *
     * @return string
     */
    public function getSAcctbal()
    {
        return $this->sAcctbal;
    }

    /**
     * Set sComment
     *
     * @param string $sComment
     *
     * @return Supplier
     */
    public function setSComment($sComment)
    {
        $this->sComment = $sComment;
    
        return $this;
    }

    /**
     * Get sComment
     *
     * @return string
     */
    public function getSComment()
    {
        return $this->sComment;
    }

    /**
     * Get sSuppkey
     *
     * @return integer
     */
    public function getSSuppkey()
    {
        return $this->sSuppkey;
    }

    /**
     * Set sNationkey
     *
     * @param \AppBundle\Entity\Nation $sNationkey
     *
     * @return Supplier
     */
    public function setSNationkey(\AppBundle\Entity\Nation $sNationkey = null)
    {
        $this->sNationkey = $sNationkey;
    
        return $this;
    }

    /**
     * Get sNationkey
     *
     * @return \AppBundle\Entity\Nation
     */
    public function getSNationkey()
    {
        return $this->sNationkey;
    }

    /**
     * Add psPartkey
     *
     * @param \AppBundle\Entity\Part $psPartkey
     *
     * @return Supplier
     */
    public function addPsPartkey(\AppBundle\Entity\Part $psPartkey)
    {
        $this->psPartkey[] = $psPartkey;
    
        return $this;
    }

    /**
     * Remove psPartkey
     *
     * @param \AppBundle\Entity\Part $psPartkey
     */
    public function removePsPartkey(\AppBundle\Entity\Part $psPartkey)
    {
        $this->psPartkey->removeElement($psPartkey);
    }

    /**
     * Get psPartkey
     *
     * @return \Doctrine\Common\Collections\Collection
     */
    public function getPsPartkey()
    {
        return $this->psPartkey;
    }
}
