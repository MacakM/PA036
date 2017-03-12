<?php

namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
 * Region
 *
 * @ORM\Table(name="REGION")
 * @ORM\Entity
 */
class Region
{
    /**
     * @var string
     *
     * @ORM\Column(name="R_NAME", type="string", length=25, nullable=false)
     */
    private $rName;

    /**
     * @var string
     *
     * @ORM\Column(name="R_COMMENT", type="string", length=152, nullable=true)
     */
    private $rComment;

    /**
     * @var integer
     *
     * @ORM\Column(name="R_REGIONKEY", type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="IDENTITY")
     */
    private $rRegionkey;



    /**
     * Set rName
     *
     * @param string $rName
     *
     * @return Region
     */
    public function setRName($rName)
    {
        $this->rName = $rName;
    
        return $this;
    }

    /**
     * Get rName
     *
     * @return string
     */
    public function getRName()
    {
        return $this->rName;
    }

    /**
     * Set rComment
     *
     * @param string $rComment
     *
     * @return Region
     */
    public function setRComment($rComment)
    {
        $this->rComment = $rComment;
    
        return $this;
    }

    /**
     * Get rComment
     *
     * @return string
     */
    public function getRComment()
    {
        return $this->rComment;
    }

    /**
     * Get rRegionkey
     *
     * @return integer
     */
    public function getRRegionkey()
    {
        return $this->rRegionkey;
    }
}
