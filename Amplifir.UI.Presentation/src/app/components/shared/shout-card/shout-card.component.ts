import { Component, OnInit, Input } from '@angular/core';

import { Shout } from '../../../services/apiClient.service';

@Component({
  selector: 'app-shout-card',
  templateUrl: './shout-card.component.html',
  styleUrls: ['./shout-card.component.scss']
})
export class ShoutCardComponent implements OnInit {

  constructor() { }

  @Input() shout: Shout = new Shout();

  ngOnInit() {
    // Ignore error.
    this.shout.createDate = new Date( this.shout.createDate ).toLocaleString();
  }

}
