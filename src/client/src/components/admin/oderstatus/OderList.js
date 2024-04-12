// UserList.js

import React from 'react';
import { List, Datagrid, TextField, EditButton, DeleteButton } from 'react-admin';
import { useDataProvider } from '../../../provider/dataProvider';

const UserList = (props) => {
  return (
    <List {...props} title="User List" perPage={10} exporter={false}>
      <Datagrid>
        <TextField source="statusCode" />
        <TextField source="statusDate" />
        <EditButton basePath="/users" />
        <DeleteButton basePath="/users" />
      </Datagrid>
    </List>
  );
};

export default UserList;
