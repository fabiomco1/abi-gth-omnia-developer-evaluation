import { Routes } from '@angular/router';
import { Component } from '@angular/core';
import { Users } from './app/components/users/users';
import { Sales } from './app/components/sales/sales';
import { Products } from './app/components/products/products';
import { Fabiooliveira } from './app/components/fabiooliveira/fabiooliveira';

export const routes: Routes = [
  { path: 'users', component: Users },
  { path: 'sales', component: Sales },
  { path: 'products', component: Products },
  { path: 'fabiooliveira', component: Fabiooliveira },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products' }
];