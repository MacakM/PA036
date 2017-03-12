<?php

namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
 * Part
 *
 * @ORM\Table(name="PART")
 * @ORM\Entity
 */
class Part
{
    /**
     * @var string
     *
     * @ORM\Column(name="P_NAME", type="string", length=55, nullable=false)
     */
    private $pName;

    /**
     * @var string
     *
     * @ORM\Column(name="P_MFGR", type="string", length=25, nullable=false)
     */
    private $pMfgr;

    /**
     * @var string
     *
     * @ORM\Column(name="P_BRAND", type="string", length=10, nullable=false)
     */
    private $pBrand;

    /**
     * @var string
     *
     * @ORM\Column(name="P_TYPE", type="string", length=25, nullable=false)
     */
    private $pType;

    /**
     * @var integer
     *
     * @ORM\Column(name="P_SIZE", type="integer", nullable=false)
     */
    private $pSize;

    /**
     * @var string
     *
     * @ORM\Column(name="P_CONTAINER", type="string", length=10, nullable=false)
     */
    private $pContainer;

    /**
     * @var string
     *
     * @ORM\Column(name="P_RETAILPRICE", type="decimal", precision=15, scale=2, nullable=false)
     */
    private $pRetailprice;

    /**
     * @var string
     *
     * @ORM\Column(name="P_COMMENT", type="string", length=23, nullable=false)
     */
    private $pComment;

    /**
     * @var integer
     *
     * @ORM\Column(name="P_PARTKEY", type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="IDENTITY")
     */
    private $pPartkey;

    /**
     * @var \Doctrine\Common\Collections\Collection
     *
     * @ORM\ManyToMany(targetEntity="AppBundle\Entity\Supplier", inversedBy="psPartkey")
     * @ORM\JoinTable(name="partsupp",
     *   joinColumns={
     *     @ORM\JoinColumn(name="PS_PARTKEY", referencedColumnName="P_PARTKEY")
     *   },
     *   inverseJoinColumns={
     *     @ORM\JoinColumn(name="PS_SUPPKEY", referencedColumnName="S_SUPPKEY")
     *   }
     * )
     */
    private $psSuppkey;

    /**
     * Constructor
     */
    public function __construct()
    {
        $this->psSuppkey = new \Doctrine\Common\Collections\ArrayCollection();
    }


    /**
     * Set pName
     *
     * @param string $pName
     *
     * @return Part
     */
    public function setPName($pName)
    {
        $this->pName = $pName;
    
        return $this;
    }

    /**
     * Get pName
     *
     * @return string
     */
    public function getPName()
    {
        return $this->pName;
    }

    /**
     * Set pMfgr
     *
     * @param string $pMfgr
     *
     * @return Part
     */
    public function setPMfgr($pMfgr)
    {
        $this->pMfgr = $pMfgr;
    
        return $this;
    }

    /**
     * Get pMfgr
     *
     * @return string
     */
    public function getPMfgr()
    {
        return $this->pMfgr;
    }

    /**
     * Set pBrand
     *
     * @param string $pBrand
     *
     * @return Part
     */
    public function setPBrand($pBrand)
    {
        $this->pBrand = $pBrand;
    
        return $this;
    }

    /**
     * Get pBrand
     *
     * @return string
     */
    public function getPBrand()
    {
        return $this->pBrand;
    }

    /**
     * Set pType
     *
     * @param string $pType
     *
     * @return Part
     */
    public function setPType($pType)
    {
        $this->pType = $pType;
    
        return $this;
    }

    /**
     * Get pType
     *
     * @return string
     */
    public function getPType()
    {
        return $this->pType;
    }

    /**
     * Set pSize
     *
     * @param integer $pSize
     *
     * @return Part
     */
    public function setPSize($pSize)
    {
        $this->pSize = $pSize;
    
        return $this;
    }

    /**
     * Get pSize
     *
     * @return integer
     */
    public function getPSize()
    {
        return $this->pSize;
    }

    /**
     * Set pContainer
     *
     * @param string $pContainer
     *
     * @return Part
     */
    public function setPContainer($pContainer)
    {
        $this->pContainer = $pContainer;
    
        return $this;
    }

    /**
     * Get pContainer
     *
     * @return string
     */
    public function getPContainer()
    {
        return $this->pContainer;
    }

    /**
     * Set pRetailprice
     *
     * @param string $pRetailprice
     *
     * @return Part
     */
    public function setPRetailprice($pRetailprice)
    {
        $this->pRetailprice = $pRetailprice;
    
        return $this;
    }

    /**
     * Get pRetailprice
     *
     * @return string
     */
    public function getPRetailprice()
    {
        return $this->pRetailprice;
    }

    /**
     * Set pComment
     *
     * @param string $pComment
     *
     * @return Part
     */
    public function setPComment($pComment)
    {
        $this->pComment = $pComment;
    
        return $this;
    }

    /**
     * Get pComment
     *
     * @return string
     */
    public function getPComment()
    {
        return $this->pComment;
    }

    /**
     * Get pPartkey
     *
     * @return integer
     */
    public function getPPartkey()
    {
        return $this->pPartkey;
    }

    /**
     * Add psSuppkey
     *
     * @param \AppBundle\Entity\Supplier $psSuppkey
     *
     * @return Part
     */
    public function addPsSuppkey(\AppBundle\Entity\Supplier $psSuppkey)
    {
        $this->psSuppkey[] = $psSuppkey;
    
        return $this;
    }

    /**
     * Remove psSuppkey
     *
     * @param \AppBundle\Entity\Supplier $psSuppkey
     */
    public function removePsSuppkey(\AppBundle\Entity\Supplier $psSuppkey)
    {
        $this->psSuppkey->removeElement($psSuppkey);
    }

    /**
     * Get psSuppkey
     *
     * @return \Doctrine\Common\Collections\Collection
     */
    public function getPsSuppkey()
    {
        return $this->psSuppkey;
    }
}
