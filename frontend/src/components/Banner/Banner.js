import React from 'react'
import "./Banner.css"

function Banner() {
  return (
    <div className='banner'>
					<img className='background-image' src="/images/banner-pic.jpg" alt="banner" />
					<section className='hero-header-text'>
					 <img
						id="ninja-landing"
						src="/images/ninja-landing.svg"
						alt="ninja-landing"
					/>
					<h1>Find tilbuddene, før din nabo gør det!</h1>
					<img
						id="ninja-rightside"
						src="/images/ninja-about.svg"
						alt="ninja-rightside"
					/>
					</section>
    </div>
  )
}

export default Banner