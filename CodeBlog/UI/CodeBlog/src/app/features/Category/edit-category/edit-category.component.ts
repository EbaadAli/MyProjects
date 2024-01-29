import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLinkActive } from '@angular/router';
import { Subscribable, Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { UpdateCategoryRequest } from '../models/update-category-request.model';


@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit, OnDestroy{

 id: string | null = null;
 category?: Category;

  paramsSubcription?: Subscription;
  editCategorySubscription?: Subscription;

  constructor(private route:ActivatedRoute, private categoryService:CategoryService, private router: Router){

  }

  ngOnInit(): void {
    this.paramsSubcription = this.route.paramMap.subscribe({
      next: (params) => {
        // id is the variable we assigned in the routing module
        // its after the colon..
        this.id = params.get('id');

        //when we get the idea send to service to get data from the api

        if(this.id)
        {
          this.categoryService.getCategoryById(this.id).subscribe({
            next: (response) => {
              this.category = response;
            }
          })
        }

      }
    });
  }

  onFormSubmit(): void{
    const UpdateCategoryRequest: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    //pass object to the service
    if(this.id)
    {
      this.editCategorySubscription = this.categoryService.updateCategory(this.id, UpdateCategoryRequest)
      .subscribe({
        next: (response) => {
            this.router.navigateByUrl('/admin/categories');
        }
      })
    }

  }

  ngOnDestroy(): void {
    this.paramsSubcription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }


}
