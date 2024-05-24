'use client';
import React, { useEffect } from "react";
import { Modal, Form, Input, Radio } from "antd";

export interface Employee
{
    businessEntityId : string;
    firstName : string;
    lastName : string;
    departmentId : string;
}

interface prop
{
    visible : any;
    setModalVisible : any;
    employee: Employee
}

export const CreateForm = (props: prop) => {
    const { visible, setModalVisible, employee } = props;


    const [form] = Form.useForm();

    const saveData = async () => {
        const employePost = { 
                firstName: form.getFieldValue("firstName"), 
                lastName: form.getFieldValue("lastName"), 
                businessEntityId: employee.businessEntityId,
            };

        const id = employee.businessEntityId; 
            
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json; charset=UTF-8' },
            body: JSON.stringify( employePost )
        };

        const response = await fetch('http://localhost:5124/Person', requestOptions);
        const data = await response.json();
    };

    form.setFieldsValue({
        firstName: employee.firstName,
        lastName: employee.lastName,
    }); 
    

    const handleSave = (values) => {
        console.log('Success:', values);
        form
            .validateFields()
            .then (() => 
                {
                    saveData();
                    setModalVisible(false);
                }
            )
            .catch((info) => {
                console.log("Validate Failed:", info);
            });
    };

    return (
        <Modal
            visible={visible}
            title="Person's detaile"
            okText="Save"
            onCancel={() => {
                setModalVisible(false);
            }}
            onOk={handleSave}
        >
            <Form form={form} layout="vertical">
                <Form.Item
                    label="First Name"
                    name="firstName"
                    rules={[
                        { required: true, message: "Please input the first name of the person!" }
                    ]}
                >
                    <Input/>
                </Form.Item>

                <Form.Item name="lastName" label="Last Name">
                    <Input/>
                </Form.Item>

                <Form.Item name="type" label="Type">
                    <Radio.Group>
                        <Radio value="public">Public</Radio>
                        <Radio value="private">Private</Radio>
                    </Radio.Group>
                </Form.Item>
            </Form>
        </Modal>
    );
};
