import { Store, createState, withProps, select } from '@ngneat/elf';
import {
  persistState,
  sessionStorageStrategy,
} from '@ngneat/elf-persist-state';

export interface Task {
	  id:number, taskTitle:string, taskDescription:string, taskPriority:string, taskStatus:string, taskCreatedDate:string, taskDeadline:string,assignee:string;
  }
  
const { state, config } = createState(withProps<Task[]>([]));

export const tasksStore = new Store({ state, name:"tasks", config });

/*export const persist = persistState(usersStore, {
  key: 'users',
  storage: sessionStorageStrategy,
});*/