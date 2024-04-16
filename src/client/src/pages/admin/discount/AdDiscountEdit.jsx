  import React, { useEffect, useState } from "react";
  import LayoutCommon from "../../../components/admin/common/LayoutCommon";
  import {
    getDiscountById,
    newCreateAndUpdateDiscount,
  } from "../../../api/Discount";
  import { useSnackbar } from "notistack";
  import { useNavigate, useParams } from "react-router-dom";
  import BoxEdit from "../../../components/admin/edit/BoxEdit";
  import { Button, Form } from "react-bootstrap";
  import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
  import { faRightToBracket, faSave } from "@fortawesome/free-solid-svg-icons";
  import BtnError from "../../../components/common/BtnError";
  import { format } from 'date-fns';


  const AdDiscountEdit = () => {
    const [validated, setValidated] = useState(false);
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();
    const initialState = {
        id: 0,
        discountPrice: 0,
        startDate: "",
        endDate: "",
        status: "",
        productId: 0,
      },
      [discount, setDiscount] = useState(initialState);

    const navigate = useNavigate();

    let { id } = useParams();
    id = id ?? 0;

    // useEffect(() => {
    //   document.title = "Thêm, cập nhật Discount";
    //   getDiscountById(id).then((data) => {
    //     if (data) {
    //       setDiscount(data);
    //     } else {
    //       setDiscount(initialState);
    //     }
    //   });
    // }, []);

    useEffect(() => {
      document.title = "Thêm, cập nhật Discount";
      getDiscountById(id).then((data) => {
        if (data) {
          // Chuyển đổi định dạng ngày tháng
          const formattedStartDate = format(new Date(data.startDate), 'yyyy-MM-dd');
          const formattedEndDate = format(new Date(data.endDate), 'yyyy-MM-dd');
          
          setDiscount({
            ...data,
            startDate: formattedStartDate,
            endDate: formattedEndDate
          });
        } else {
          setDiscount(initialState);
        }
      });
    }, []);

    const handleSubmit = (e) => {
      e.preventDefault();
      if (e.currentTarget.checkValidity() === false) {
        e.stopPropagation();
        setValidated(true);
      } else {
        let data = new FormData(e.target);

        newCreateAndUpdateDiscount(id, data).then((data) => {
          if (data) {
            enqueueSnackbar("Đã lưu thành công", {
              variant: "success",
            });
            navigate(`/admin/discount`);
          } else {
            enqueueSnackbar("Đã xảy ra lỗi khi lưu", {
              variant: "error",
            });
          }
        });
      }
    };

    return (
      <LayoutCommon>
        <div className="wrapper">
          <h3 className="text-success py-3">Thêm/cập nhật Voucher Discount</h3>
          <Form
            method="post"
            encType="multipart/form-data"
            onSubmit={handleSubmit}
            noValidate
            validated={validated}
          >
            <Form.Control type="hidden" name="id" value={discount.id} />

            <BoxEdit
              label={"Mã Voucher Discount"}
              control={
                <Form.Control
                  type="text"
                  name="status"
                  title="Mã voucher Discount"
                  required
                  value={discount.status || ""}
                  onChange={(e) =>
                    setDiscount({ ...discount, status: e.target.value })
                  }
                />
              }
              notempty={"Không được bỏ trống"}
            />

            <BoxEdit
              label={"Số tiền giảm (VNĐ)"}
              control={
                <Form.Control
                  type="number"
                  name="discountPrice"
                  title="discount Price"
                  required
                  value={discount.discountPrice || ""}
                  onChange={(e) =>
                    setDiscount({ ...discount, discountPrice: e.target.value })
                  }
                />
              }
              notempty={"Không được bỏ trống"}
            />

            <BoxEdit
              label={"Ngày bắt đầu"}
              control={
                <Form.Control
                  type="date"
                  name="startDate"
                  title="Start Date"
                  required
                  value={discount.startDate || ""}
                  onChange={(e) =>
                    setDiscount({ ...discount, startDate: e.target.value })
                  }
                />
              }
              notempty={"Không được bỏ trống"}
            />

            <BoxEdit
              label={"Ngày hết hạn"}
              control={
                <Form.Control
                  type="date"
                  name="endDate"
                  title="End Date"
                  required
                  value={discount.endDate || ""}
                  onChange={(e) =>
                    setDiscount({ ...discount, endDate: e.target.value })
                  }
                />
              }
              notempty={"Không được bỏ trống"}
            />

            <BoxEdit
              label={"Mã sản phẩm áp dụng"}
              control={
                <Form.Control
                  type="number"
                  name="productId"
                  title="Product Id"
                  required
                  value={discount.productId || ""}
                  onChange={(e) =>
                    setDiscount({ ...discount, productId: e.target.value })
                  }
                />
              }
              notempty={"Không được bỏ trống"}
            />

            <div className="text-center">
              <Button variant="success" type="submit">
                Lưu các thay đổi
                <FontAwesomeIcon icon={faSave} className="px-1" />
              </Button>

              <BtnError
                icon={faRightToBracket}
                slug={"/admin/Discount"}
                name="Hủy và quay lại"
              />
            </div>
          </Form>
        </div>
      </LayoutCommon>
    );
  };
  export default AdDiscountEdit;
