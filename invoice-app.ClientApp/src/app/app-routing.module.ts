import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component'; 

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' }, 
  { path: 'invoices', loadChildren: './receipts/receipts.module#ReceiptsModule'  },
  { path: 'quotations', loadChildren: './receipts/receipts.module#ReceiptsModule'  },
  { path: 'receipts', loadChildren: './receipts/receipts.module#ReceiptsModule'  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
