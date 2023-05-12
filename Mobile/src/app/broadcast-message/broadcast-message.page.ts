import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-broadcast-message',
  templateUrl: './broadcast-message.page.html',
  styleUrls: ['./broadcast-message.page.scss'],
})
export class BroadcastMessagePage implements OnInit {
  public messages: any = [];

  constructor(private api: ApiService) { }

  ngOnInit() {

    this.api.selectTableData('BroadcastMessages').then((results: any) => {
      if (results.length > 0) {
        this.messages = results;
      }
      else {
        this.messages.push({ MessageText: 'No data found' });
      }
    });
  }

}
