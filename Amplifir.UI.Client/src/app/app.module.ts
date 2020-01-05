import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProfileComponent } from './components/pages/profile/profile.component';
import { FeedComponent } from './components/pages/feed/feed.component';
import { SettingsComponent } from './components/pages/settings/settings.component';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { AuthModalComponent } from './components/modals/auth-modal/auth-modal.component';
import { ModalsComponent } from './components/modals/modals.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ProfileComponent,
    FeedComponent,
    SettingsComponent,
    PageHeaderComponent,
    AuthModalComponent,
    ModalsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
