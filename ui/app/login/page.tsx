'use client'

import { useState, useEffect, useMemo } from 'react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Button, Form, Input, Radio } from "antd";
import useCookie from 'react-use-cookie';
import { InitObvject } from "../infrastructure/initObject"

export default function Login() {

  const [userToken, setUserToken] = useCookie('myToken', '0');

  const [] = useState({ userName: "", password: "" });
  const [submitDisable, setSubmitDisabled] = useState(true);
  const [form] = Form.useForm();

  const submit = async () => {

    const requestOptions = {
      method: 'POST',
    };

    const response = await fetch(`${InitObvject.domain}/api/Person/Login?user=${form.getFieldValue("user")}&password=${form.getFieldValue("password")}`, requestOptions);

    const data = await response.json();

    setUserToken(data);

    alert("You have authorized!");
  };


  const onChange = () =>
    {
      setSubmitDisabled(form.getFieldValue("user").length === 0 || form.getFieldValue("password").length ===0);
    }

  return (
    <div className="ag-theme-alpine" style={{ height: '600px' }}>
      <Form form={form} layout="vertical">
        <Form.Item style={{ width: "25%", height: 90, margin: 0 }}
          label="User"
          name="user"
          rules={[
            { required: true, message: "Please input the user name" }
          ]}
        >
          <Input onChange={onChange}/>
        </Form.Item>

        <Form.Item name="password" label="Password" style={{ width: "25%", height: 90, margin: 0 }}
          rules={[
            { required: true, message: "Please input the user password" }
          ]}>
          <Input onChange={onChange}/>
        </Form.Item>

        <Form.Item name="submit" label="Submit" >
          <Button type="primary" disabled={submitDisable} onClick={submit}>Submit</Button>
        </Form.Item>
      </Form>
    </div>
  );
}