import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { TrendsService } from 'src/app/services/trends.service';
import { UserService } from 'src/app/services/user.service';
import { Order } from '../dtos/order';
import { Stock } from '../dtos/stock';
import { User } from '../dtos/user';


import { registerLocaleData } from '@angular/common';
import localeIt from '@angular/common/locales/it'
import { ToastrService } from 'ngx-toastr';
registerLocaleData(localeIt, 'it');

@Component({
  selector: 'app-user',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  user = new User();
  order = new Order();  
  stocks: Stock[] = [];
  selectStock = new Stock();
  userId = 0;
  userAssets = [];

  constructor(
    private userService: UserService,
    private orderService: OrderService,
    private trendsService: TrendsService,
    private route: ActivatedRoute,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.userId = this.route.snapshot.params["userId"];
    this.getUser(this.userId);

    this.trendsService.getStocks().subscribe((stocks) => {
      this.stocks = stocks;
    });
  }

  getUser(userId: number) {
    this.userService.getUser(userId).subscribe((user) => {
      this.user = user;
      this.userAssets = user.userAssets;
    });
  }

  showSelectStock(stock: Stock) {
    this.selectStock = stock;
    this.order.symbol = stock.symbol;
  }

  executeOrder(order: Order) {
    this.orderService.executeOrder(order, this.userId).subscribe(
      (response) => {
        if (!response)
          return;

        this.toastr.success(response);
        this.getUser(this.userId);
      },
    )
  }  
}
