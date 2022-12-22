import {Component, OnInit} from '@angular/core';
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;

  constructor(public userService: UserService) {

  }

  ngOnInit(): void {
    this.userService.checkAuthentication()
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout(){
    window.location.href = "api/user/logout";
  }
}
