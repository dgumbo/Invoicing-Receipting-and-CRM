import { Component, OnInit, Inject } from '@angular/core';

@Component({
  selector: 'app-receipt-list',
  templateUrl: './receipt-list.component.html',
  styleUrls: ['./receipt-list.component.scss']
})
export class ReceiptListComponent implements OnInit {
   
  constructor( @Inject('BASE_URL') public baseUrl: string ) { }

  ngOnInit(  ) {  }

}
