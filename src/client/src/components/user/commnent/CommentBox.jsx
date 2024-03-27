import React, { useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import BoxEdit from "../../admin/edit/BoxEdit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSave } from "@fortawesome/free-solid-svg-icons";
import { useNavigate, useParams } from "react-router-dom";
import { useSnackbar } from "notistack";
import { createNewAndUpdateComment } from "../../../api/Comment";

const CommentBox = () => {
  // const [commnetText, setCommentText ] = useState('');
  const [customerId, setCustomerId] = useState(null);
  const [validated, setValidated] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const initialState = {
      id: 0,
      name: "",
      rating: "",
      commentText: "",
      customerId: 0,
      productId: 0,
    },
    [commnet, setCommnet] = useState(initialState);

  const navigate = useNavigate();

  let { id } = useParams();
  id = id ?? 0;

  useEffect(() => {}, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      let data = new FormData(e.target);

      createNewAndUpdateComment(id, data).then((data) => {
        if (data) {
          enqueueSnackbar("Đã lưu thành công", {
            variant: "success",
          });
          navigate(`/home`);
        } else {
          enqueueSnackbar("Đã xảy ra lỗi khi lưu", {
            variant: "error",
          });
        }
      });
    }
  };

  return (
    <section>
      <div className="comment_content">
        <h5 className="comment_title">Để lại bình luận của bạn</h5>
      </div>
      <div className="comment_box">
        <Form
          method="post"
          encType="multipart/form-data"
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={commnet.id} />

          <BoxEdit
            label={"Tên bạn"}
            control={
              <Form.Control
                type="text"
                name="name"
                title="Name"
                required
                value={commnet.name || ""}
                onChange={(e) =>
                  setCommnet({ ...commnet, name: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Rating"}
            control={
              <Form.Control
                type="text"
                name="rating"
                title="Rating"
                required
                value={commnet.rating || ""}
                onChange={(e) =>
                  setCommnet({ ...commnet, rating: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />
          <BoxEdit
            label={"Cusommer"}
            control={
              <Form.Control
                type="text"
                name="commentText"
                title="Comment Text"
                required
                value={commnet.commentText || ""}
                onChange={(e) =>
                  setCommnet({ ...commnet, commentText: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />
          <BoxEdit
            label={"Comment Text"}
            control={
              <Form.Control
                type="text"
                name="customerId"
                title="customer Id"
                required
                value={commnet.customerId || ""}
                onChange={(e) =>
                  setCommnet({ ...commnet, customerId: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"product Id"}
            control={
              <Form.Control
                type="text"
                name="productId"
                title="product Id"
                required
                value={commnet.productId || ""}
                onChange={(e) =>
                  setCommnet({ ...commnet, productId: e.target.value })
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
          </div>
        </Form>
      </div>
    </section>
  );
};
export default CommentBox;
