import { Routes } from '@angular/router';
import { ScorePageComponent } from './pages/score-page/score-page.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/scores',
    pathMatch: 'full',
  },
  {
    path: 'scores',
    component: ScorePageComponent,
  },
];
