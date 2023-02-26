import torch
import cv2
import time
import numpy as np

#model_type = 'DPT_Large'
model_type = 'DPT_Hybrid'
#model_type = 'MiDaS_small'

midas = torch.hub.load('intel-isl/MiDaS', model_type)

device = torch.device('cuda') if torch.cuda.is_available() else torch.device('cpu')
midas.to(device)
midas.eval()

midas_transforms = torch.hub.load('intel-isl/MiDaS', 'transforms')

if model_type == 'DPT_Large' or model_type == 'DPT_Hybrid':
    transform = midas_transforms.dpt_transform
else:
    transform = midas_transforms.small_transform

def estimateDepthInImage(file):
    img = cv2.imread(file, 0)
    img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

    input_bash = transform(img_rgb).to(device)

    with torch.no_grad():
        prediction = midas(input_bash)

        prediction = torch.nn.functional.interpolate(
            prediction.unsqueeze(1),
            size=img.shape[:2],
            mode='bicubic',
            align_corners=False
        ).squeeze()

    depth_map = prediction.cpu().numpy()
    depth_map = cv2.normalize(depth_map, None, 0, 1, norm_type=cv2.NORM_MINMAX, dtype=cv2.CV_64F)

    depth_map = (depth_map*255).astype(np.uint8)
    #depth_map = cv2.applyColorMap(depth_map, cv2.COLORMAP_MAGMA)

    cv2.imwrite('src/output.png', depth_map)
