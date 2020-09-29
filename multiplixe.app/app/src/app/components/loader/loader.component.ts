import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base.component';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html'
})
export class LoaderComponent extends BaseComponent implements OnInit {

  public show: boolean = false;
  public text: string = 'Aguarde';

  constructor() {
    super();

    this.addSubscription(
      this.eventEmitter
        .get('loader')
        .subscribe(o => {
          this.show = o.show;
          this.text = o.text || this.text;
        }));
  }

  ngOnInit() { }

}
