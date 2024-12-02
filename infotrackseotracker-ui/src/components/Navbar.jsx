import React from "react";
import { Link } from "react-router-dom";
import '../styles/Navbar.css';

const Navbar = () => {
    return (
        <nav className="navbar">
            <div className="navbar-title">InfoTrack SEO Tracker</div>
            <ul className="navbar-links">
                <li><Link to="/">Search</Link></li>
                <li><Link to="/history">History</Link></li>
            </ul>
        </nav>
    );
};

export default Navbar;