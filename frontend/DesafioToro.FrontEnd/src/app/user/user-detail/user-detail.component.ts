import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { User } from '../dtos/user';

@Component({
  selector: 'app-user',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  user: User = new User();

  constructor(private userService: UserService, private route: ActivatedRoute ) {
  }

  ngOnInit(): void {
    let userId = this.route.snapshot.params["userId"];

    this.userService.getUser(userId).subscribe((user) => {
      this.user = user;
    })
  }

}
