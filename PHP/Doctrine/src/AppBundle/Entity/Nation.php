<?php

namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
 * Nation
 *
 * @ORM\Table(name="NATION", indexes={@ORM\Index(name="IDX_3CA2AAA1719A3B4D", columns={"N_REGIONKEY"})})
 * @ORM\Entity
 */
class Nation
{
    /**
     * @var string
     *
     * @ORM\Column(name="N_NAME", type="string", length=25, nullable=false)
     */
    private $nName;

    /**
     * @var string
     *
     * @ORM\Column(name="N_COMMENT", type="string", length=152, nullable=true)
     */
    private $nComment;

    /**
     * @var integer
     *
     * @ORM\Column(name="N_NATIONKEY", type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="IDENTITY")
     */
    private $nNationkey;

    /**
     * @var \AppBundle\Entity\Region
     *
     * @ORM\ManyToOne(targetEntity="AppBundle\Entity\Region")
     * @ORM\JoinColumns({
     *   @ORM\JoinColumn(name="N_REGIONKEY", referencedColumnName="R_REGIONKEY")
     * })
     */
    private $nRegionkey;



    /**
     * Set nName
     *
     * @param string $nName
     *
     * @return Nation
     */
    public function setNName($nName)
    {
        $this->nName = $nName;
    
        return $this;
    }

    /**
     * Get nName
     *
     * @return string
     */
    public function getNName()
    {
        return $this->nName;
    }

    /**
     * Set nComment
     *
     * @param string $nComment
     *
     * @return Nation
     */
    public function setNComment($nComment)
    {
        $this->nComment = $nComment;
    
        return $this;
    }

    /**
     * Get nComment
     *
     * @return string
     */
    public function getNComment()
    {
        return $this->nComment;
    }

    /**
     * Get nNationkey
     *
     * @return integer
     */
    public function getNNationkey()
    {
        return $this->nNationkey;
    }

    /**
     * Set nRegionkey
     *
     * @param \AppBundle\Entity\Region $nRegionkey
     *
     * @return Nation
     */
    public function setNRegionkey(\AppBundle\Entity\Region $nRegionkey = null)
    {
        $this->nRegionkey = $nRegionkey;
    
        return $this;
    }

    /**
     * Get nRegionkey
     *
     * @return \AppBundle\Entity\Region
     */
    public function getNRegionkey()
    {
        return $this->nRegionkey;
    }
}
