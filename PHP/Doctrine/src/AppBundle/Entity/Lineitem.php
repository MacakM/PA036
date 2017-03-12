<?php

namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
 * Lineitem
 *
 * @ORM\Table(name="LINEITEM", indexes={@ORM\Index(name="IDX_670893F250B201C6", columns={"L_PARTKEY"}), @ORM\Index(name="IDX_670893F2CBF9FE1", columns={"L_SUPPKEY"}), @ORM\Index(name="IDX_670893F28B8417B", columns={"L_ORDERKEY"})})
 * @ORM\Entity
 */
class Lineitem
{
    /**
     * @var string
     *
     * @ORM\Column(name="L_QUANTITY", type="decimal", precision=15, scale=2, nullable=false)
     */
    private $lQuantity;

    /**
     * @var string
     *
     * @ORM\Column(name="L_EXTENDEDPRICE", type="decimal", precision=15, scale=2, nullable=false)
     */
    private $lExtendedprice;

    /**
     * @var string
     *
     * @ORM\Column(name="L_DISCOUNT", type="decimal", precision=15, scale=2, nullable=false)
     */
    private $lDiscount;

    /**
     * @var string
     *
     * @ORM\Column(name="L_TAX", type="decimal", precision=15, scale=2, nullable=false)
     */
    private $lTax;

    /**
     * @var string
     *
     * @ORM\Column(name="L_RETURNFLAG", type="string", length=1, nullable=false)
     */
    private $lReturnflag;

    /**
     * @var string
     *
     * @ORM\Column(name="L_LINESTATUS", type="string", length=1, nullable=false)
     */
    private $lLinestatus;

    /**
     * @var \DateTime
     *
     * @ORM\Column(name="L_SHIPDATE", type="date", nullable=false)
     */
    private $lShipdate;

    /**
     * @var \DateTime
     *
     * @ORM\Column(name="L_COMMITDATE", type="date", nullable=false)
     */
    private $lCommitdate;

    /**
     * @var \DateTime
     *
     * @ORM\Column(name="L_RECEIPTDATE", type="date", nullable=false)
     */
    private $lReceiptdate;

    /**
     * @var string
     *
     * @ORM\Column(name="L_SHIPINSTRUCT", type="string", length=25, nullable=false)
     */
    private $lShipinstruct;

    /**
     * @var string
     *
     * @ORM\Column(name="L_SHIPMODE", type="string", length=10, nullable=false)
     */
    private $lShipmode;

    /**
     * @var string
     *
     * @ORM\Column(name="L_COMMENT", type="string", length=44, nullable=false)
     */
    private $lComment;

    /**
     * @var integer
     *
     * @ORM\Column(name="L_LINENUMBER", type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="NONE")
     */
    private $lLinenumber;

    /**
     * @var \AppBundle\Entity\Orders
     *
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="NONE")
     * @ORM\OneToOne(targetEntity="AppBundle\Entity\Orders")
     * @ORM\JoinColumns({
     *   @ORM\JoinColumn(name="L_ORDERKEY", referencedColumnName="O_ORDERKEY")
     * })
     */
    private $lOrderkey;

    /**
     * @var \AppBundle\Entity\Part
     *
     * @ORM\ManyToOne(targetEntity="AppBundle\Entity\Part")
     * @ORM\JoinColumns({
     *   @ORM\JoinColumn(name="L_PARTKEY", referencedColumnName="P_PARTKEY")
     * })
     */
    private $lPartkey;

    /**
     * @var \AppBundle\Entity\Supplier
     *
     * @ORM\ManyToOne(targetEntity="AppBundle\Entity\Supplier")
     * @ORM\JoinColumns({
     *   @ORM\JoinColumn(name="L_SUPPKEY", referencedColumnName="S_SUPPKEY")
     * })
     */
    private $lSuppkey;



    /**
     * Set lQuantity
     *
     * @param string $lQuantity
     *
     * @return Lineitem
     */
    public function setLQuantity($lQuantity)
    {
        $this->lQuantity = $lQuantity;
    
        return $this;
    }

    /**
     * Get lQuantity
     *
     * @return string
     */
    public function getLQuantity()
    {
        return $this->lQuantity;
    }

    /**
     * Set lExtendedprice
     *
     * @param string $lExtendedprice
     *
     * @return Lineitem
     */
    public function setLExtendedprice($lExtendedprice)
    {
        $this->lExtendedprice = $lExtendedprice;
    
        return $this;
    }

    /**
     * Get lExtendedprice
     *
     * @return string
     */
    public function getLExtendedprice()
    {
        return $this->lExtendedprice;
    }

    /**
     * Set lDiscount
     *
     * @param string $lDiscount
     *
     * @return Lineitem
     */
    public function setLDiscount($lDiscount)
    {
        $this->lDiscount = $lDiscount;
    
        return $this;
    }

    /**
     * Get lDiscount
     *
     * @return string
     */
    public function getLDiscount()
    {
        return $this->lDiscount;
    }

    /**
     * Set lTax
     *
     * @param string $lTax
     *
     * @return Lineitem
     */
    public function setLTax($lTax)
    {
        $this->lTax = $lTax;
    
        return $this;
    }

    /**
     * Get lTax
     *
     * @return string
     */
    public function getLTax()
    {
        return $this->lTax;
    }

    /**
     * Set lReturnflag
     *
     * @param string $lReturnflag
     *
     * @return Lineitem
     */
    public function setLReturnflag($lReturnflag)
    {
        $this->lReturnflag = $lReturnflag;
    
        return $this;
    }

    /**
     * Get lReturnflag
     *
     * @return string
     */
    public function getLReturnflag()
    {
        return $this->lReturnflag;
    }

    /**
     * Set lLinestatus
     *
     * @param string $lLinestatus
     *
     * @return Lineitem
     */
    public function setLLinestatus($lLinestatus)
    {
        $this->lLinestatus = $lLinestatus;
    
        return $this;
    }

    /**
     * Get lLinestatus
     *
     * @return string
     */
    public function getLLinestatus()
    {
        return $this->lLinestatus;
    }

    /**
     * Set lShipdate
     *
     * @param \DateTime $lShipdate
     *
     * @return Lineitem
     */
    public function setLShipdate($lShipdate)
    {
        $this->lShipdate = $lShipdate;
    
        return $this;
    }

    /**
     * Get lShipdate
     *
     * @return \DateTime
     */
    public function getLShipdate()
    {
        return $this->lShipdate;
    }

    /**
     * Set lCommitdate
     *
     * @param \DateTime $lCommitdate
     *
     * @return Lineitem
     */
    public function setLCommitdate($lCommitdate)
    {
        $this->lCommitdate = $lCommitdate;
    
        return $this;
    }

    /**
     * Get lCommitdate
     *
     * @return \DateTime
     */
    public function getLCommitdate()
    {
        return $this->lCommitdate;
    }

    /**
     * Set lReceiptdate
     *
     * @param \DateTime $lReceiptdate
     *
     * @return Lineitem
     */
    public function setLReceiptdate($lReceiptdate)
    {
        $this->lReceiptdate = $lReceiptdate;
    
        return $this;
    }

    /**
     * Get lReceiptdate
     *
     * @return \DateTime
     */
    public function getLReceiptdate()
    {
        return $this->lReceiptdate;
    }

    /**
     * Set lShipinstruct
     *
     * @param string $lShipinstruct
     *
     * @return Lineitem
     */
    public function setLShipinstruct($lShipinstruct)
    {
        $this->lShipinstruct = $lShipinstruct;
    
        return $this;
    }

    /**
     * Get lShipinstruct
     *
     * @return string
     */
    public function getLShipinstruct()
    {
        return $this->lShipinstruct;
    }

    /**
     * Set lShipmode
     *
     * @param string $lShipmode
     *
     * @return Lineitem
     */
    public function setLShipmode($lShipmode)
    {
        $this->lShipmode = $lShipmode;
    
        return $this;
    }

    /**
     * Get lShipmode
     *
     * @return string
     */
    public function getLShipmode()
    {
        return $this->lShipmode;
    }

    /**
     * Set lComment
     *
     * @param string $lComment
     *
     * @return Lineitem
     */
    public function setLComment($lComment)
    {
        $this->lComment = $lComment;
    
        return $this;
    }

    /**
     * Get lComment
     *
     * @return string
     */
    public function getLComment()
    {
        return $this->lComment;
    }

    /**
     * Set lLinenumber
     *
     * @param integer $lLinenumber
     *
     * @return Lineitem
     */
    public function setLLinenumber($lLinenumber)
    {
        $this->lLinenumber = $lLinenumber;
    
        return $this;
    }

    /**
     * Get lLinenumber
     *
     * @return integer
     */
    public function getLLinenumber()
    {
        return $this->lLinenumber;
    }

    /**
     * Set lOrderkey
     *
     * @param \AppBundle\Entity\Orders $lOrderkey
     *
     * @return Lineitem
     */
    public function setLOrderkey(\AppBundle\Entity\Orders $lOrderkey)
    {
        $this->lOrderkey = $lOrderkey;
    
        return $this;
    }

    /**
     * Get lOrderkey
     *
     * @return \AppBundle\Entity\Orders
     */
    public function getLOrderkey()
    {
        return $this->lOrderkey;
    }

    /**
     * Set lPartkey
     *
     * @param \AppBundle\Entity\Part $lPartkey
     *
     * @return Lineitem
     */
    public function setLPartkey(\AppBundle\Entity\Part $lPartkey = null)
    {
        $this->lPartkey = $lPartkey;
    
        return $this;
    }

    /**
     * Get lPartkey
     *
     * @return \AppBundle\Entity\Part
     */
    public function getLPartkey()
    {
        return $this->lPartkey;
    }

    /**
     * Set lSuppkey
     *
     * @param \AppBundle\Entity\Supplier $lSuppkey
     *
     * @return Lineitem
     */
    public function setLSuppkey(\AppBundle\Entity\Supplier $lSuppkey = null)
    {
        $this->lSuppkey = $lSuppkey;
    
        return $this;
    }

    /**
     * Get lSuppkey
     *
     * @return \AppBundle\Entity\Supplier
     */
    public function getLSuppkey()
    {
        return $this->lSuppkey;
    }
}
