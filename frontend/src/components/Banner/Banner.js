import React from 'react'
import "./Banner.css"

function Banner() {

  return (

    <div className='banner-container'>
				<div className="slogan">
					<img
						id="ninja-landing"
						src="/images/ninja-landing.svg"
						alt="ninja-landing"
					/>
					<i>Find tilbuddene, før din nabo gør det!</i>
					<img
						id="ninja-rightside"
						src="/images/ninja-about.svg"
						alt="ninja-rightside"
					/>
				</div>
    </div>
  )
}

export default Banner