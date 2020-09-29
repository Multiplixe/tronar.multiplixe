import { Component, OnInit, Input } from '@angular/core';
import { BaseComponent } from '../base.component';

@Component({
  selector: 'app-list-items',
  templateUrl: './list-items.component.html'
})
export class ListItemsComponent extends BaseComponent implements OnInit {

  @Input()
  public items: string[] = [];

  @Input()
  public bgColorClass: string = '';
   
  constructor() {
    super();
  }

  ngOnInit() {
    super.ngOnInit();
  }

}
