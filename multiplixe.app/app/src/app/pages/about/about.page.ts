import { Component, OnInit } from '@angular/core';
import { BasePage } from '../base.page';

@Component({
  selector: 'app-about',
  templateUrl: './about.page.html',
  styleUrls: ['./about.page.scss'],
})
export class AboutPage extends BasePage implements OnInit {

  constructor() { 
    super();
  }

  ngOnInit() {
  }

}
