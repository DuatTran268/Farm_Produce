import { useSnackbar } from "notistack";
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { Button, Form } from "react-bootstrap";
import BoxEdit from "../../../components/admin/edit/BoxEdit";
import { faRightToBracket, faSave } from "@fortawesome/free-solid-svg-icons";
import BtnError from "../../../components/common/BtnError";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  createNewAndUpdateComment,
  getCommnetById,
} from "../../../api/Comment";
import { format } from "date-fns";



const AdCommentEdit = () => {
  const [validated, setValidated] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const initialState = {
      id: 0,
      name: "",
      rating: 0,
      created: "",
      commentText: "",
      status: false,
      applicationUserId: "",
      productId: 0,
    },
    [comment, setCommnet] = useState(initialState);

  const navigate = useNavigate();

  let { id } = useParams();
  id = id ?? 0;
  
  const [refreshData, setRefreshData] = useState(false);

  useEffect(() => {
    document.title = "Chi tiết bình luận, đánh giá";
    
    getCommnetById(id).then((data) => {
      if (data) {
        // Chuyển đổi định dạng ngày tháng
        const formattedDateCreate = format(new Date(data.created), 'yyyy-MM-dd');
        console.log("Check dataa of comment: ",data)
        setCommnet({
          ...data,
          created: formattedDateCreate,
        });
      } else {
        setCommnet(initialState);
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
      console.log("Form data:");
      for (const pair of data.entries()) {
        console.log(pair[0] + ': ' + pair[1]);
      }

      createNewAndUpdateComment(id, data).then((data) => {
        if (data) {
          enqueueSnackbar("Đã lưu thành công", {
            variant: "success",
          });
          setRefreshData((prev) => !prev);
          navigate(`/admin/comment`);
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
        <h3 className="text-success py-3">Chi tiết bình luận, đánh giá</h3>
        <Form
          method="post"
          encType="multipart/form-data"
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={comment.id} />

          <BoxEdit
            label={"Tên người đánh giá"}
            control={
              <Form.Control
                type="text"
                name="name"
                title="Name"
                required
                value={comment.name || ""}
                onChange={(e) =>
                  setCommnet({ ...comment, name: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Số sao đánh giá"}
            control={
              <Form.Control
                type="text"
                name="rating"
                title="rating"
                required
                value={comment.rating || ""}
                onChange={(e) =>
                  setCommnet({ ...comment, rating: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Ngày bình luận"}
            control={
              <Form.Control
              // disabled={true}
                type="date"
                name="created"
                title="Created"
                value={comment.created || ""}
                onChange={(e) =>
                  setCommnet({ ...comment, created: e.target.value })
                }
              />
            }
          />

          <BoxEdit
            label={"Comment Text"}
            control={
              <Form.Control
                type="text"
                name="commentText"
                title="Comment Text"
                value={comment.commentText || ""}
                onChange={(e) =>
                  setCommnet({ ...comment, commentText: e.target.value })
                }
              />
            }
          />

          <BoxEdit
            label={"Mã người dùng"}
            control={
              <Form.Control
              // disabled={true}
                type="text"
                name="applicationUserId"
                title="applicationUserId"
                value={comment.applicationUserId || ""}
                onChange={(e) =>
                  setCommnet({ ...comment, applicationUserId: e.target.value })
                }
              />
            }
          />

          <BoxEdit
            label={"Sản phẩm"}
            control={
              <Form.Control
                // disabled={true}
                type="text"
                name="productId"
                title="product Id"
                value={comment.productId || ""}
                onChange={(e) =>
                  setCommnet({ ...comment, productId: e.target.value })
                }
              />
            }
          />

          {/* <div className="row mb-3">
            <div className="col-sm-10 offset-sm-2">
              <div className="form-check">
                <input
                  className="form-check-input"
                  type="checkbox"
                  name="status"
                  checked={comment.status}
                  title="Công khai comment"
                  onChange={(e) => {
                    setCommnet({ ...comment, status: e.target.checked });
                    console.log(e.target.checked);
                  }}
                />
                <Form.Label className="form-check-label">Công khai</Form.Label>
              </div>
            </div>
          </div> */}

          <div className="text-center">
            <Button variant="success" type="submit">
              Lưu các thay đổi
              <FontAwesomeIcon icon={faSave} className="px-1" />
            </Button>

            <BtnError
              icon={faRightToBracket}
              slug={"/admin/comment"}
              name="Hủy và quay lại"
            />
          </div>
        </Form>
      </div>
    </LayoutCommon>
  );
};
export default AdCommentEdit;
