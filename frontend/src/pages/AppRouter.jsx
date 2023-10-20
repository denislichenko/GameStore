import React from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import { Home } from './Home';
import { Consoles } from './Consoles';
import { Accessories } from './Accessories';
import { Games } from './Games';
import { Cart } from './Cart';
import { Orders } from './Orders';
import { SignUp } from './auth/SignUp';
import { LogIn } from './auth/LogIn';

const withContext = (Component) => {
  return function mixin() {
    return (
      <Component />
    );
  };
};

const AppRouter = () => {
  return (
    <Routes>
      <Route path='/' element={<Home />} />
      <Route path='/consoles' element={<Consoles />} />
      <Route path='/accessories' element={<Accessories />} />
      <Route path='/games' element={<Games />} />
      <Route path='/cart' element={<Cart />} />
      <Route path='/orders' element={<Orders />} />
      <Route path='/sign-up' element={<SignUp />} />
      <Route path='/login' element={<LogIn />} />
      <Route path='*' element={<Navigate to='/' replace />} />
    </Routes>
  );
};

export default withContext(AppRouter);
