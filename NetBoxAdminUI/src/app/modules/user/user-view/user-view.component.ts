import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ResponseResult } from '../../../core/models/response-result.model';
import { SearchRequestModel } from '../../../core/models/search-request.model';
import { ModalService } from '../../../core/services/modal.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { ConfirmationModalComponent } from '../../shared/components/confirmation-modal/confirmation-modal.component';
import { UserModel } from '../models/user.model';
import { UserService } from '../services/user.service';
import { isEmpty } from '../../../core/extenstion/helper';
import { AppPermissions } from '../../../core/extenstion/permissions';
import { UserCreateComponent } from '../user-create/user-create.component';

@Component({
  selector: 'netbox-user-view',
  templateUrl: './user-view.component.html',
  styleUrl: './user-view.component.scss'
})
export class UserViewComponent implements OnInit {

  isDisplay = false;
  isBlocked = false;
  isEdit = false;
  focus: any;

  userId: string = '';
  pageSizeOptions: number[] = [10, 25, 50, 100];

  userModels: UserModel[];
  searchModel = new SearchRequestModel(10, 1);

  p = AppPermissions;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('userCreateComponent') userCreateComponent: UserCreateComponent;


  constructor(private userService: UserService, private toasterService: ToasterService, private modalService: ModalService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  public pageChanged(event: PageEvent): void {
    this.searchModel.pageSize = event.pageSize
    this.searchModel.pageNumber = event.pageIndex + 1;
    this.getUsers();
  }

  viewCreateUser() {
     this.isDisplay = true;

    this.userCreateComponent.show();
  }

  viewEditUser(userId: string) {
    this.userCreateComponent.edit(userId);
  }

  setDisplay() {
    this.isDisplay = false;
    this.userId = "";
    this.isBlocked = false;
  }

  clearSearchTerm() {
    this.searchModel.searchTerm = '';
    this.search();
  }

  getUsers() {
    this.isBlocked = true;
    this.userService.getAll(this.searchModel).subscribe({
      next: (users: ResponseResult<UserModel[]>) => {
        this.userModels = users.data;
        this.searchModel.totalRecords = users.totalRecordCount;
        this.isBlocked = false;
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.toasterService.error(err);
      }
    });
  }

  search() {
    if (!isEmpty(this.searchModel.searchTerm) && this.searchModel.searchTerm.length > 2)
      this.getUsers();
    else if (isEmpty(this.searchModel.searchTerm)) {
      this.getUsers();
    }
  }

  deleteUser(userId: string) {
    let options = {
      title: 'Delete User',
      message: 'Are you sure you want to delete the user ?',
    };
    this.modalService.displayDialog(ConfirmationModalComponent, options)
    this.modalService.confirmed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isBlocked = true;
        this.userService.deleteUser(userId)
          .subscribe({
            next: (res: any) => {
              this.isBlocked = false;
              this.toasterService.success("User has been successfully deleted");
              this.getUsers();
            },
            error: (err: ErrorResponse) => {
              this.isBlocked = false;
              this.toasterService.error(err);
            }
          })
      }
    });
  }
}
