import {ApplicationConfig, InjectionToken, provideZoneChangeDetection} from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import {provideHttpClient, withJsonpSupport} from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {environment} from '../environments/environment.development';

export interface AppConfig {
  apiBaseUrl: string;
}

export const APP_CONFIG = new InjectionToken<AppConfig>('app.config');

export const DEFAULT_APP_CONFIG: AppConfig = {
  apiBaseUrl: environment.apiBaseUrl,
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withJsonpSupport()),
    provideAnimationsAsync(),
    {
      provide: APP_CONFIG,
      useValue: DEFAULT_APP_CONFIG
    }
  ]
};
