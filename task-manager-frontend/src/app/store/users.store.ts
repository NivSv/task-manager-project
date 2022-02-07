import { Store, createState, withProps, select } from '@ngneat/elf';
import {
  persistState,
  sessionStorageStrategy,
} from '@ngneat/elf-persist-state';

export interface User {
	 id:number, username:string;
  }
  
const { state, config } = createState(withProps<User[]>([]));

export const usersStore = new Store({ state, name:"users", config });

/*export const persist = persistState(usersStore, {
  key: 'users',
  storage: sessionStorageStrategy,
});*/