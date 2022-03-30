import React from 'react'
import "./Footer.css"

function Footer() {

  return (

    <div className='footer'>
        <h4 id='footerLink__about'>Kontakt os - Hvem er vi? - Betingelser </h4>
        <p id='footerText'>&copy;{new Date().getFullYear()} PRIS NINJA INC </p>
    </div>
  )
}

export default Footer