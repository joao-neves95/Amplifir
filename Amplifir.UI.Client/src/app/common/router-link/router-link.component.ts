import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-router-link',
  templateUrl: './router-link.component.html',
  styleUrls: ['./router-link.component.scss']
})
export class RouterLinkComponent implements OnInit {

  constructor() { }

  classes: string = 'nav-link';

  @Input() link: string = '';
  @Input() label: string = '';
  @Input() activeClass: string = 'active';

  ngOnInit() {
  }

}
