import React from 'react'
import "./Banner.css"

function Banner() {
  return (
    <div className='banner'>
		<div className='banner-filler'></div>
					<img className='background-image' src="/images/banner-pic.png" alt="banner" />
					<section className='hero-header-text'>
					</section>
					<div className='banner-title'><img
						id="ninja-landing"
						src="/images/ninja-landing.svg"
						alt="ninja-landing"
					/>
					<h1>Find tilbuddene, før din nabo gør det!</h1>
					<img
						id="ninja-rightside"
						src="/images/ninja-about.svg"
						alt="ninja-rightside"
					/></div>
    </div>
  )
}

export default Banner