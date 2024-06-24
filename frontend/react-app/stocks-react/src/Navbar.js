import React from 'react'
import './App.css';

function Navbar() {
  return (
    <nav>
      <div className="navbar">
        <div className="logo"><a href="index.html">Stocks</a></div>
        <div className="nav-links">
            <a href="index.html">Home</a>
            <a href="services.html">Services</a>
            <a href="about.html">About</a>
            <a href="contact.html">Contact</a>
        </div>
      </div>
  </nav>
  )
}

export default Navbar
