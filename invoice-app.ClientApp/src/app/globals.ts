import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class Globals {

  public readonly hostUrl: string = environment.apiUrl;
  public readonly production: boolean = environment.production;
  public admin: boolean = false;
  public serviceCategories:  any[] = [];

  public isCkEditorJsLoaded: boolean = false;

  //constructor(private viewsDataService: ViewsDataService) {
  //  //console.log("Globals Constructor Initing!!"); 

  //  if (localStorage.getItem('currentUser')) {
  //    this.activateAdmin();
  //  }
  //  this.setServiceCategories();
  //}

  //public setServiceCategories() {
  //  //console.log("num run time");
  //  this.viewsDataService.getAllViews("ServiceCategory")
  //    .subscribe((res: ServiceCategory[]) => this.serviceCategories = res);
  //}

  public prepareDocumentUrl(filename: string): string {
    //console.log(filename);
    return "/storage/view?filename=" + filename;
  }

  public activateAdmin() {
    this.admin = true;
  }

  public deActivateAdmin() {
    this.admin = false;
  }
}
