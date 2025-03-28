import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './pages/app.config';
import { AppComponent } from './pages/app.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
