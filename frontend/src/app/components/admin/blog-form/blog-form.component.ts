import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { CategoryService } from '../../../category.service';
import { Category } from '../../../types/category';

@Component({
  selector: 'app-blog-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSelectModule,
  ],
  templateUrl: './blog-form.component.html',
  styleUrl: './blog-form.component.scss',
})
export class BlogFormComponent {
  formBuilder = inject(FormBuilder);
  blogForm = this.formBuilder.group({
    id: [null],
    title: ['', [Validators.required]],
    categoryId: ['', [Validators.required]],
    description: [''],
    content: ['', [Validators.required]],
    image: [''],
    isFeatured: [false],
  });
  categoryService = inject(CategoryService);
  categoryList: Category[] = [];
  ngOnInit() {
    this.categoryService
      .getCategoryList()
      .subscribe((result) => (this.categoryList = result));
  }
  create() {
    console.log(this.blogForm.value);
  }
}
