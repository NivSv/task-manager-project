import {User, usersStore} from './users.store'

export const setUsers = (users : User[]) => {
	usersStore.update((state) => users);
}

export const getUsers = () => usersStore.getValue();
