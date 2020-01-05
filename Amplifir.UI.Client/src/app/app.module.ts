import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { FeedComponent } from './pages/feed/feed.component';
import { SettingsComponent } from './pages/settings/settings.component';
import { PageHeaderComponent } from './page-header/page-header.component';
import { AuthModalComponent } from './modals/auth-modal/auth-modal.component';
import { ModalsComponent } from './modals/modals.component';

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
