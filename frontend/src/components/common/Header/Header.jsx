import React from 'react';
import { Link } from 'react-router-dom';

export const Header = () => {
  return (
    <div className='flex justify-center items-center'>
      <div className='p-4'>L.O.G.O.</div>
      <Link to='/'>
        <div className='p-4'>Home</div>
      </Link>
      <Link to='/consoles'>
        <div className='p-4'>Consoles</div>
      </Link>
      <Link to='/accessories'>
        <div className='p-4'>Accessories</div>
      </Link>
      <Link to='/games'>
        <div className='p-4'>Games</div>
      </Link>
      <Link to='/orders'>
        <div className='p-4'>Orders</div>
      </Link>
      <Link to='/cart'>
        <div className='p-4'>Cart</div>
      </Link>
      <Link to='/sign-up'>
        <div className='p-4'>Sign up</div>
      </Link>
      <Link to='/login'>
        <div className='p-4'>Log In</div>
      </Link>
    </div>
  );
};
