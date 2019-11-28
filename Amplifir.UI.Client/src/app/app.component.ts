import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  pageTitle: string = '';

  changePageTile(newTitle: string) {
    this.pageTitle = newTitle;
  }
}
