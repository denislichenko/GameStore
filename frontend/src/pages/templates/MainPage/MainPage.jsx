import React from 'react';
import { Header } from '../../../components/common/Header';
import { Footer } from '../../../components/common/Footer';

export const MainPage = ({ children }) => {
  return (
    <div>
      <Header/>
      {children}
      <Footer/>
    </div>
  );
};
