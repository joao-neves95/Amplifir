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
  createDate: string = new Date().toLocaleString();

  ngOnInit() {
    this.createDate = this.shout.createDate.toLocaleString();
  }

}
