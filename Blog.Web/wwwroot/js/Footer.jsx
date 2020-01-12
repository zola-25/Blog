import React from 'react';


const Footer = () => {
    return (
        <> 
            <hr />
            <footer>
                <p>&copy; {(new Date().getFullYear())} - Mike Towill</p>
            </footer>
        </>
    );
};

export default Footer;
