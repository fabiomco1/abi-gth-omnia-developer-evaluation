import 'zone.js';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { App } from './app/app';
import { routes } from './app.routes';
import { appConfig } from '../src/app/app.config';

bootstrapApplication(App, appConfig)
  .catch((err) => console.error(err));
