import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: Array<any> = [];

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((users) => {
      this.users = users;
      console.log(users);
    })
  }

  showUserDetails(userId: number) {
    this.router.navigate([`./user/${userId}`]);

  }

}
