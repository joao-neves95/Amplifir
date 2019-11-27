import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProfileComponent } from './pages/profile/profile.component';
import { FeedComponent } from './pages/feed/feed.component';
import { SettingsComponent } from './pages/settings/settings.component';

const routes: Routes = [
  { path: '', redirectTo: 'feed', pathMatch: 'full' },
  { path: 'feed', component: FeedComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'settings', component: SettingsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
